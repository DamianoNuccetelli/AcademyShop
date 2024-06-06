import React, { useState } from "react";
import NavbarProfile from "../NavbarProfile/NavbarProfile";
import UtenteDetails from "../UtenteDetails/UtenteDetails";
import EliminaUtente from "../EliminaUtente/EliminaUtente";

import "./HomeProfile.css";

import editUtenteImg from "../../../img/EditUtente.png";
import deleteUtenteImg from "../../../img/elimina_utente.png";

const nome = sessionStorage.getItem("nome");
const cognome = sessionStorage.getItem("cognome");

const HomeProfile = () => {
  const [activeComponent, setActiveComponent] = useState("Dettagli");

  const renderComponent = () => {
    switch (activeComponent) {
      case "Dettagli":
        return <UtenteDetails />;
      case "Elimina":
        return <EliminaUtente />;
      default:
        return <UtenteDetails />;
    }
  };

  const getImageSrc = () => {
    switch (activeComponent) {
      case "Dettagli":
        return editUtenteImg;
      case "Elimina":
        return deleteUtenteImg;
      default:
        return editUtenteImg;
    }
  };

  return (
    <div className="header">
      <div className="title_container_user">
        <div className="title_text">
          <h1>Il mio profilo:</h1>
          <h2>{nome} {cognome}</h2>
        </div>
      </div>
      <div className="flex_utente">
        <NavbarProfile
          setActiveComponent={setActiveComponent}
          activeComponent={activeComponent}
        />
        {renderComponent()}
        <div className="image_section">
          <img
            setActiveComponent={setActiveComponent}
            activeComponent={activeComponent}
            src={getImageSrc()}
            alt="Edit Utente"
            className="user_image"
          />
        </div>
      </div>
    </div>
  );
};

export default HomeProfile;
