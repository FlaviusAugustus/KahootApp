import React, { useState, useEffect } from 'react';
import * as signalr from '@microsoft/signalr';
import useConnection from "../Hooks/useConnection";
import isConnected from "../Utils/ConnectionUtils";
import {useNavigate} from "react-router-dom";

type GameInfo = {
    groupID: string,
    HostConnectionID: string 
};

const HostComponent = () => {
    const [connection, setConnection] = useConnection()
    const [gameInfo, setGameInfo] = useState<GameInfo>();
    const [userNames, setUserNames] = useState<string[]>([]);
    const navigator = useNavigate();
    
    useEffect(() => {
        async function connectToHub(): Promise<void> {
            if(!connection || isConnected(connection))
                return
            await connection.start();
            
            connection.on('onGroupCreated', groupInfo => {
                let host = groupInfo.hostConnectionID;
                let group = groupInfo.groupID;
                let game: GameInfo = {
                    groupID: group,
                    HostConnectionID: host
                };  
                setGameInfo(game);
            });
            connection.on('playerJoined', userName => {
                setUserNames(UserNames => [...UserNames, userName]);
            });
        }
        connectToHub();
    }, [connection, userNames, setGameInfo]);
    
    const onClick = () => {
        connection && isConnected(connection) && connection.invoke("CreateGroup", "pokoj");
        setUserNames([]);
    }
    
    const StartGameHandler = () => {
        connection && isConnected(connection) && connection.invoke("StartGame", gameInfo?.HostConnectionID);
        navigator(`/game/${gameInfo?.HostConnectionID}`, {
            state: {
                userNames: userNames, 
                gameInfo: gameInfo
        }});
    }
    
    return <div>
        <button onClick={onClick}>Create Group</button>
        <button onClick={StartGameHandler}>Start game</button>
        To Join Input: {gameInfo && gameInfo.groupID}
        <div>{userNames.map((username) => <li>{username}</li>)}</div> 
    </div>
}
export default HostComponent