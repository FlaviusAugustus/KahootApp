import * as signalr from '@microsoft/signalr';

export default function isConnected(connection: signalr.HubConnection): boolean {
    return connection.state == signalr.HubConnectionState.Connected;
}