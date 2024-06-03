import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUserCircle, faShoppingCart , faBoxOpen  } from '@fortawesome/free-solid-svg-icons';

import './Navbar.css';


const Navbar = () => {
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
                <h4 className='mb-20'>Prodotti</h4>
                <FontAwesomeIcon icon={faBoxOpen } className="user-icon mb-20" />
                <h4 className='mb-20'>Ordini</h4>
            </div>
            <div className="navbar_container">
                
            </div>
        </div>
    );
};

export default Navbar;


