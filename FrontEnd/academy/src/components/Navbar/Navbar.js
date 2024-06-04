import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUserCircle, faShoppingCart , faBoxOpen  } from '@fortawesome/free-solid-svg-icons';
import { useNavigate } from "react-router-dom";
import './Navbar.css';



const Navbar = () => {

    let navigate = useNavigate(); 
    const NavigateOrdini = () =>{ 
    let path = `/Ordine`; 
    navigate(path);
}
    const NavigateProdotti = () =>{
        let path = `/Dashboard`
        navigate(path)
    }

    return (
        <div className="navbar">
            <div className="navbar_container">
                <FontAwesomeIcon icon={faUserCircle} className="user-icon" />
                <h4>Profilo</h4>
           </div>
           <div className='center'>
                <hr className="separator" />
           </div>
           <div className="navbar_container">
                <FontAwesomeIcon icon={faShoppingCart} className="user-icon mb-20" />
                <h4 className='mb-20'onClick={NavigateProdotti}>Prodotti</h4>
                <FontAwesomeIcon icon={faBoxOpen } className="user-icon mb-20" />
                <h4 className='mb-20'onClick={NavigateOrdini}>Ordini</h4>
            </div>
            <div className="navbar_container">
                
            </div>
        </div>
    );
};

export default Navbar;


