import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUserCircle, faShoppingCart , faBoxOpen  } from '@fortawesome/free-solid-svg-icons';
import { useNavigate } from "react-router-dom";
import './Navbar.css';

import logoAS from '../../img/LOGOacademyshop.png';

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
                <img src={logoAS} alt="LogoAS" className='logoAS' />
            </div>
            <div className='center'>
                <hr className="separator" />
            </div>
            <div className="navbar_container">
            <div className='profilo'>
                    <FontAwesomeIcon icon={faUserCircle} className="user-icon mb-20" />
                    <h4 className='mb-20'>Profilo</h4>
                </div>
                <div className='prodotti'>
                    <FontAwesomeIcon icon={faShoppingCart} className="user-icon mb-20" />
                    <h4 className='mb-20'>Prodotti</h4>
                </div>
                <div className='ordini'>
                    <FontAwesomeIcon icon={faBoxOpen } className="user-icon mb-20" />
                    <h4 className='mb-20'>Ordini</h4>
                </div>
            </div>
            <div className="navbar_container">
                
            </div>
        </div>
    );
};

export default Navbar;


