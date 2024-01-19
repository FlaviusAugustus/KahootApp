import {useState} from "react";
import HostComponent from "./HostComponent";
import PlayerComponent from "./PlayerComponent";

export {}

const GamemodeComponent = () => {
    const [HostComponentChoosen, setHostComponentChosen] = useState<boolean>()
    
    const Onclick = (value: boolean) => {
        setHostComponentChosen(value)
    }
    
    return(
        <div>
            <button onClick={() => Onclick(true)}>Host a Game</button>
            <button onClick={() => Onclick(false)}>Join a Game</button>
            {HostComponentChoosen && <HostComponent/>}
            {!HostComponentChoosen && <PlayerComponent/>}
        </div>
    )
}

export default GamemodeComponent;