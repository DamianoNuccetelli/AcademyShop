import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Navbar from '../Navbar/Navbar';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import ContentOrdine from './OrdineComponent/ContentOrdine';
//import 'bootstrap/dist/css/bootstrap.css';
import './Ordine.css';

const Ordine = () => {
    return (

        <div className='ordine'>
            <Navbar/>
            <ContentOrdine />
        </div>
    );
};

export default Ordine;


