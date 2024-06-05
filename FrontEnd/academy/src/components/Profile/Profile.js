import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBox, faPenToSquare, faMultiply } from '@fortawesome/free-solid-svg-icons'; 
import './Profile.css';
import Modal from 'react-modal';
import Navbar from '../Navbar/Navbar';
import User from '../User/User';

const Profile = () => {

        return (
            <div className='dashboard'>
                <Navbar/>
                <User/>
            </div>
        );
};

export default Profile;
