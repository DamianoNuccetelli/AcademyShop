import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUserCircle, faShoppingCart, faBoxOpen } from '@fortawesome/free-solid-svg-icons';
import { useNavigate, useLocation } from "react-router-dom";

import './Navbar.css';

import logoAS from '../../img/LOGOacademyshop.png';

const Navbar = () => {
  let navigate = useNavigate();
  
  const NavigateOrdini = () => {
    let path = `/Ordine`;
    navigate(path);
  }

  const NavigateProdotti = () => {
    let path = `/Dashboard`
    navigate(path)
  }

  const NavigateProfile = () => {
    let path = `/Profile`;
    navigate(path);
  }

  const [classeProdotti, setClasseProdotti] = useState('prodotti');
  const [blueIconProdotti, setBlueIconProdotti] = useState('user-icon');

  const [classeOrdini, setClasseOrdini] = useState('ordini');
  const [blueIconOrdini, setBlueIconOrdini] = useState('user-icon');

  const [classeProfile, setClasseProfile] = useState('profile');
  const [blueIconProfile, setBlueIconProfile] = useState('user-icon');

  const location = useLocation();

  useEffect(() => {
    if (location.pathname === '/Dashboard') {
      setClasseProdotti('selected');
      setClasseOrdini('ordini');
      setClasseProfile('profilo');
      setBlueIconProdotti('user-icon-selected');
      setBlueIconOrdini('user-icon');
      setBlueIconProfile('user-icon');
    } else if (location.pathname === '/Ordine') {
      setClasseProdotti('prodotti');
      setClasseOrdini('selected');
      setClasseProfile('profilo');
      setBlueIconProdotti('user-icon');
      setBlueIconOrdini('user-icon-selected');
      setBlueIconProfile('user-icon');
    } else if (location.pathname === '/Profile') {
      setClasseProdotti('prodotti');
      setClasseOrdini('ordini');
      setClasseProfile('profilo selected');
      setBlueIconProdotti('user-icon');
      setBlueIconOrdini('user-icon');
      setBlueIconProfile('user-icon-selected');
    }
  }, [location]);

  return (
    <div className="navbar">
      <div className="navbar_container">
        <img src={logoAS} alt="LogoAS" className='logoAS' />
      </div>
      <div className='center'>
        <hr className="separator" />
      </div>
      <div className="navbar_container">
        <div className={classeProfile} onClick={NavigateProfile}>
          <FontAwesomeIcon icon={faUserCircle}  className={blueIconProfile}/>
          <h4 className='mb-20'>Profilo</h4>
        </div>
        <div className={classeProdotti} onClick={NavigateProdotti}>
          <FontAwesomeIcon icon={faShoppingCart} className={blueIconProdotti} />
          <h4 className='mb-20'>Prodotti</h4>
        </div>
        <div className={classeOrdini} onClick={NavigateOrdini}>
          <FontAwesomeIcon icon={faBoxOpen} className={blueIconOrdini} />
          <h4 className='mb-20'>Ordini</h4>
        </div>
      </div>
      <div className="navbar_container">
      </div>
    </div>
  );
};

export default Navbar;
