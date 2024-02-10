import { useNavigate } from 'react-router-dom';

const GamemodeComponent = () => {
    const navigate = useNavigate();
    
    return(
        <div>
            <button onClick={() => navigate('/host')}>Host a Game</button>
            <button onClick={() => navigate('/join')}>Join a Game</button>
        </div>
    )
}

export default GamemodeComponent;