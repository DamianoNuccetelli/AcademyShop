import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUserCircle } from '@fortawesome/free-solid-svg-icons';

import './Navbar.css';


const Navbar = () => {
    return (
        <div className="navbar">
            <div className="navbar_container">
                <FontAwesomeIcon icon={faUserCircle} className="user-icon" />
                <h3>Profilo</h3>
           </div>
           <div className='center'>
                <hr className="separator" />
           </div>
           <div className="navbar_container">
                <FontAwesomeIcon icon={faUserCircle} className="user-icon" />
                <h3>Prodotti</h3>
                <FontAwesomeIcon icon={faUserCircle} className="user-icon" />
                <h3>Ordini</h3>
           </div>
        </div>
    );
};

export default Navbar;


