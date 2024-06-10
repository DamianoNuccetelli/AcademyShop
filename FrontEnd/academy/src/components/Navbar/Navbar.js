import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUserCircle, faShoppingCart, faBoxOpen, faSignOutAlt } from '@fortawesome/free-solid-svg-icons';
import { useNavigate, useLocation } from "react-router-dom";

import './Navbar.css';

import logoAS from '../../img/LOGOacademyshop.png';
import SfondoErrorAcademyShop from '../../img/SfondoErrorAcademyShop.png';

const Navbar = () => {
  let navigate = useNavigate();

  const [showLogoutModal, setShowLogoutModal] = useState(false); 

  const openLogoutModal = () => {
    setShowLogoutModal(true);
  };

  const closeLogoutModal = () => {
    setShowLogoutModal(false);
  };

  const confirmLogout = () => {
    localStorage.clear();
    navigate('/Login');
  };

  const NavigateOrdini = () => {
    let path = `/Ordine`;
    navigate(path);
  };

  const NavigateProdotti = () => {
    let path = `/Dashboard`
    navigate(path);
  };

  const NavigateProfile = () => {
    let path = `/Profile`;
    navigate(path);
  };

  const Logout = () => {
    openLogoutModal(); 
  };

  const [classeProdotti, setClasseProdotti] = useState('prodotti');
  const [blueIconProdotti, setBlueIconProdotti] = useState('user-icon');

  const [classeOrdini, setClasseOrdini] = useState('ordini');
  const [blueIconOrdini, setBlueIconOrdini] = useState('user-icon');

  const [classeProfile, setClasseProfile] = useState('profile');
  const [blueIconProfile, setBlueIconProfile] = useState('user-icon');

  const location = useLocation();

  useEffect(() => {
    if (location.pathname === '/Dashboard') {
      setClasseProdotti('prodotti selected');
      setClasseOrdini('ordini');
      setClasseProfile('profilo');
      setBlueIconProdotti('user-icon-selected');
      setBlueIconOrdini('user-icon');
      setBlueIconProfile('user-icon');
    } else if (location.pathname === '/Profile') {
      setClasseProdotti('prodotti');
      setClasseOrdini('ordini');
      setClasseProfile('profilo selected');
      setBlueIconProdotti('user-icon');
      setBlueIconOrdini('user-icon');
      setBlueIconProfile('user-icon-selected');
    }else if (location.pathname === '/Ordine') {
      setClasseProdotti('prodotti');
      setClasseOrdini('ordini selected');
      setClasseProfile('profilo');
      setBlueIconProdotti('user-icon');
      setBlueIconOrdini('user-icon-selected');
      setBlueIconProfile('user-icon');
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
      <div className="navbar_container navbar_top">
        <div className={classeProfile} onClick={NavigateProfile}>
          <FontAwesomeIcon icon={faUserCircle} className={blueIconProfile}/>
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
      <div className='navbar_container logout' onClick={openLogoutModal}>
        <div className="box_flex">
          <FontAwesomeIcon icon={faSignOutAlt} className="user-icon icon_purple"/>
          <h4 className='mb-20'>Logout</h4>
        </div>
      </div>

      {showLogoutModal && (
        <div className="modal">
          <div className="modal-content">
            <img src={SfondoErrorAcademyShop} alt="error" className='LogoutModalIMG' />
            <h2>Conferma Logout</h2>
            <p>Sei sicuro di voler effettuare il logout?</p>
            <div className="modal-buttons">
              <button onClick={closeLogoutModal} className='LogoutModal'>Annulla</button>
              <button onClick={confirmLogout} className='LogoutModal '>Logout</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default Navbar;
