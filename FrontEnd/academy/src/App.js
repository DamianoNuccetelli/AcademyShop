import React from 'react';
import './App.css';
import Navbar from './components/Navbar/Navbar';
import Header from './components/Header/Header';
import Dashboard from './components/Dashboard/Dashboard';
import { Routes, Route } from 'react-router-dom';
import Login from './components/Login/Login';
import SignUp from './components/SignUp/SignUp';
import SignIn from './components/SignIn/SignIn';
import Ordine from './components/Ordine/Ordine';


const App = () => {
    return (
        <>
        <Routes>
            <Route path="/Dashboard" element={<Dashboard />}/>
            <Route path="/login" element={<Login />} />
            <Route path="/signup" element={<SignUp />} />
            <Route path="/signin" element={<SignIn />} />
            <Route path="/ordine" element={<Ordine />} />
        </Routes>
        </>
    );
};

export default App;


