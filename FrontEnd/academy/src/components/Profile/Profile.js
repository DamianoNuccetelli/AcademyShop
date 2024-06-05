import React from 'react';
import './Profile.css';
import Navbar from '../Navbar/Navbar';
import HomeProfile from './HomeProfile/HomeProfile';

const Profile = () => {
    return (
        <div className='dashboard'>
            <Navbar />
            <HomeProfile />
        </div>
    );
};

export default Profile;
