import React, { useState } from 'react';
import Modal from 'react-modal';

import './HomeProfile.css';



const HomeProfile = () => {
    
    return (
        <div className="header">
            <div className='title_container_user'>
                <div className='title_text'>
                    <h1>Il mio profilo:</h1>
                    <h2>Mario Rossi</h2>
                </div>
            </div>
        </div>
    );
};

export default HomeProfile;
