package zhuang;

import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.ConcurrentSkipListSet;

import org.cometd.bayeux.server.BayeuxServer;
import org.cometd.bayeux.server.ConfigurableServerChannel;
import org.cometd.bayeux.server.ServerMessage;
import org.cometd.bayeux.server.ServerSession;
import org.cometd.server.AbstractService;
import org.cometd.server.authorizer.GrantAuthorizer;

public class BubbleServer extends AbstractService
{
	private final ConcurrentSkipListSet<String> room = new ConcurrentSkipListSet<>(); 
	
    public BubbleServer(BayeuxServer bayeux)
    {
        super(bayeux, "chat");
        room.add("test");
        addService("/service/createRoom", "createRoom");
        addService("/service/enterRoom", "enterRoom");
    }
    public void createRoom(final ServerSession from, ServerMessage message){
        Map<String, Object> input = message.getDataAsMap();
        String roomId = (String)input.get("roomId");
        Map<String, String> msg = new HashMap<>();
        if(room.contains(roomId)){
        	msg.put("status", "REST");
        	msg.put("message", "the roomID " + roomId +" has exists...");
        }
        else{
        	room.add(roomId);
        	getBayeux().createChannelIfAbsent("/"+roomId, new ConfigurableServerChannel.Initializer(){
        		public void configureChannel(ConfigurableServerChannel channel){
        			channel.setPersistent(true);
        			channel.addAuthorizer(GrantAuthorizer.GRANT_ALL);
        		}
        	});
        	msg.put("status", "OK");
        	msg.put("message", "create room " + roomId + " success!!");
        }
    	from.deliver(getServerSession(), "/service/createRoom", msg);
    }
    
    public void enterRoom(final ServerSession from, ServerMessage message){
    	Map<String, Object> input = message.getDataAsMap();
    	String roomId = (String)input.get("channel");
    	Map<String, String> msg = new HashMap<>();
    	if(room.contains(roomId)){
    		msg.put("status", "OK");
    		msg.put("message", "enter success!!");
    	}
    	else{
    		msg.put("status", "REST");
    		msg.put("message", "there is not such a room name " + roomId);
    	}
    	from.deliver(getServerSession(), "/service/enterRoom", msg);
    }
}
