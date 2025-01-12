// NavbarProfile.js
import React from 'react';
import './NavbarProfile.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faChevronRight } from '@fortawesome/free-solid-svg-icons';

const NavbarProfile = ({ setActiveComponent, activeComponent }) => {
    return (
        <div className='box_navbar_profile'>
            <div
                className={`options-container ${activeComponent === 'Dettagli' ? 'selected-utente' : ''}`}
                onClick={() => setActiveComponent('Dettagli')}
            >
                <FontAwesomeIcon icon={faChevronRight} className='icon_navbar' />
                <h4>Dettagli</h4>
            </div>
            <div
                className={`options-container ${activeComponent === 'Elimina' ? 'selected-utente' : ''}`}
                onClick={() => setActiveComponent('Elimina')}
            >
                <FontAwesomeIcon icon={faChevronRight} className='icon_navbar' />
                <h4>Elimina</h4>
            </div>
        </div>
    );
};

export default NavbarProfile;
