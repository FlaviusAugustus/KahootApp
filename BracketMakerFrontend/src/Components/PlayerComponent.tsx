import {getSpaceUntilMaxLength} from "@testing-library/user-event/dist/utils";
import * as signalr from "@microsoft/signalr";
import {useEffect, useState} from "react";
import useConnection from "../Hooks/useConnection";

const PlayerComponent = () => {
    const [connection, setConnection] = useConnection();
    const [gameID, setGameID] = useState<string>('');
    const [userName, setUserName] = useState<string>('');
    const [started, setStarted] = useState<boolean>(false);
    const [joined, setJoined] = useState<boolean>(false);
    
    const SubmitHandler = (event: any) => {
        event.preventDefault();
        connection?.invoke('JoinGroup', gameID, userName);
    }

    useEffect(() => {
        if(connection && !started) {
            setStarted(true)
            connection.start()
                .then(res => { console.log('Connected');

                    connection.on('joinedGame', d => {
                        setJoined(true)
                    })
                });
        }
    }, [started, connection, setJoined]);
    
    return (
        <div><form onSubmit={SubmitHandler}>
            <input id="gameID" onChange={(e) => setGameID(e.target.value)}/>
            <input id="userName" onChange={(e) => setUserName(e.target.value)}/>
            <input type="submit"/>
            {joined && <div>game with ID {gameID} joined!</div>}
        </form></div>
    ) 
}


export default PlayerComponent;