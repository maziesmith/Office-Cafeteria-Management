import javax.websocket.server.ServerEndpoint;
import java.io.IOException;
import javax.websocket.OnClose;
import javax.websocket.OnError;
import javax.websocket.OnMessage;
import javax.websocket.OnOpen;
import javax.websocket.Session;

@ServerEndpoint(value = "/user")
public class StaffWebSocket {
	private static Session _session;

	public static void SendMessage(String message)
	{
		if(_session!=null)
		{
			try {     		
				_session.getBasicRemote().sendText(message);        		    		
			} catch (IOException e) {
				e.printStackTrace();
			}
		}
	}
	
	@OnOpen
    public void open(Session session) { 
        //Connection opened.
        System.out.println("EchoEndpoint on open");    
        _session = session;
    }
	
	@OnMessage
    public void onMessage(String message, Session session) {
        
        UserWebSocket.SendMessage(message);
    }
	
	@OnError
    public void error(Session session, Throwable error) { 
        //Connection error.
        System.out.println("EchoEndpoint on error");
    }

    @OnClose
    public void close(Session session) { 
        //Connection closed.
        System.out.println("EchoEndpoint on close");
    }
}
