import React, { useState, useEffect } from 'react';
import * as signalr from '@microsoft/signalr';
import useConnection from "../Hooks/useConnection";

type GameInfo = {
    groupID: string,
    HostConnectionID: string 
};

const HostComponent = () => {
    const [connection, setConnection] = useConnection()
    const [gameInfo, setGameInfo] = useState<GameInfo>();
    const [userNames, setUserNames] = useState<string[]>([])
    
    const [started, setstarted] = useState<boolean>(false);

    useEffect(() => {
        if(connection && !started) {
            setstarted(true)
            connection.start()
                .then(res => {
                    console.log('Connected');
                    connection.on('onGroupCreated', groupInfo => {
                        setGameInfo(groupInfo)
                    });

                    connection.on('playerJoined', userName  => {
                        setUserNames(UserNames => [...UserNames, userName] );
                    });
                });
        }
    }, [connection, started, userNames]);
    
    const onClick = () => {
        started && connection && connection.invoke("CreateGroup", "pokoj")
    
    }
    return <div>
        <button onClick={onClick}>Create Group</button>
        To Join Input: {gameInfo && gameInfo.groupID}
        <div>{userNames.map((username) => <li>{username}</li>)}</div> 
    </div>
}
export default HostComponent