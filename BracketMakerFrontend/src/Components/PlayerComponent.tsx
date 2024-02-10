import {getSpaceUntilMaxLength} from "@testing-library/user-event/dist/utils";
import * as signalr from "@microsoft/signalr";
import {useEffect, useState} from "react";
import useConnection from "../Hooks/useConnection";
import isConnected from "../Utils/ConnectionUtils";

const PlayerComponent = () => {
    const [connection, setConnection] = useConnection();
    const [gameID, setGameID] = useState<string>('');
    const [userName, setUserName] = useState<string>('');
    const [joined, setJoined] = useState<boolean>(false);
    const [error, setError] = useState<string>("");
    
    useEffect(() => {
        async function connectToHub(): Promise<void>
        {
            if(!connection || isConnected(connection)) 
                return;
            await connection.start();
            connection.on('joinedGame', m => {
                setJoined(true);
            });
            connection.on('failedToJoin', error => {
                setError(error);
            });
        }
        connectToHub();
    }, [connection, setJoined]);
    
    const SubmitHandler = (event: any) => {
        event.preventDefault();
        connection?.invoke('JoinGroup', gameID, userName);
    }

    return (
        <div><form onSubmit={SubmitHandler}>
            <input id="gameID" onChange={(e) => setGameID(e.target.value)}/>
            <input id="userName" onChange={(e) => setUserName(e.target.value)}/>
            <input type="submit"/>
            {joined && <div>game with ID {gameID} joined!</div>}
            {error != "" && <div>{error}</div>}
        </form></div>
    ) 
}


export default PlayerComponent;