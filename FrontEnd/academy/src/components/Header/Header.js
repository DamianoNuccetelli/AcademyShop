import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';

import './Header.css';


const Header = () => {
    return (
        <div className="header">
            <div className='title_container'>
                <h1>Dashboard</h1>
                <h2>Benvenuto, Mario</h2>
            </div>
            <div className=''>
            <div className='add_container'>
                    <h2>Add new product</h2>
                    <FontAwesomeIcon icon={faPlus} className="plus-icon" />
            </div>
        </div>
        </div>
    );
};

export default Header;


