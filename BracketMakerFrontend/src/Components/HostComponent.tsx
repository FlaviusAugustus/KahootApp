import React, { useState, useEffect } from 'react';
import * as signalr from '@microsoft/signalr';

type GameInfo = {
    groupID: string,
    HostConnectionID: string 
};

const HostComponent = () => {
    const [connection, setConnection] = useState<signalr.HubConnection>();
    const [started, setstarted] = useState<boolean>(false);
    const [gameInfo, setGameInfo] = useState<GameInfo>();

    useEffect(() => {
        const newConnection = new signalr.HubConnectionBuilder()
            .withUrl("http://localhost:5161/game",
                { skipNegotiation: true, transport: signalr.HttpTransportType.WebSockets })
            .withAutomaticReconnect()
            .build()
        
        setConnection(newConnection)
    }, [])

    useEffect(() => {
        if(connection && !started) {
            setstarted(true)
            connection.start()
                .then(res => { console.log('Connected');
            
                connection.on('onGroupCreated', groupInfo => {
                    setGameInfo(groupInfo)
                })
                });
        }
    }, [started, connection, gameInfo]);
    const onClick = () => {
        connection && connection.invoke("CreateGroup", "pokoj")
    
    }
    return <div><button onClick={onClick}>Create Group</button>To Join Input: {gameInfo && gameInfo.groupID}</div>
}
export default HostComponent