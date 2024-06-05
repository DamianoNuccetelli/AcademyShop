import React from 'react';
import './Profile.css';
import Navbar from '../Navbar/Navbar';
import HomeProfile from './HomeProfile/HomeProfile';
import NavbarProfile from './NavbarProfile/NavbarProfile';


const Profile = () => {
    return (
        <div className='dashboard'>
            <Navbar />
            <HomeProfile />
            <NavbarProfile />
        </div>
    );
};

export default Profile;

