import React, { useState } from 'react';
import NavbarProfile from '../NavbarProfile/NavbarProfile';
import DettagliUtente from '../DettagliUtente/DettagliUtente';
import EliminaUtente from '../EliminaUtente/EliminaUtente';
import './HomeProfile.css';

import editUtenteImg from '../../../img/EditUtente.png';

const HomeProfile = () => {
    const [activeComponent, setActiveComponent] = useState('Dettagli');

    const renderComponent = () => {
        switch (activeComponent) {
            case 'Dettagli':
                return <DettagliUtente />;
            case 'Elimina':
                return <EliminaUtente />;
            default:
                return <DettagliUtente />;
        }
    };

    return (
        <div className="header">
            <div className='title_container_user'>
                <div className='title_text'>
                    <h1>Il mio profilo:</h1>
                    <h2>Mario Rossi</h2>
                </div>
            </div>
            <div className='flex_utente'>
                <NavbarProfile setActiveComponent={setActiveComponent} activeComponent={activeComponent} />
                {renderComponent()}
                <div className="image_section">
                    <img src={editUtenteImg} alt="Edit Utente" className="user_image"/>
                </div>
            </div>
        </div>
    );
};

export default HomeProfile;
