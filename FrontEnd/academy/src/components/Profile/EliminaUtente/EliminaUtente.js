import React from "react";
import "./EliminaUtente.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import sessoMaschile from "../../../img/sesso maschile.png";
import { useNavigate } from "react-router-dom";
import { useState, useEffect } from "react";

const API_URL = "https://localhost:7031/users/";
//
const userSessionId = sessionStorage.getItem("userSessionId");

const EliminaUtente = () => {
  const navigate = useNavigate();
  const handleDelete = async () => {
    const userId = sessionStorage.getItem('userId'); // Retrieve userId from sessionStorage

    if (!userId) {
      console.error('userId is not available');
      console.error("User ID not found in session storage");
      return;
    }

    try {
      const response = await fetch(`https://localhost:7031/users/${userId}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
        },
        cache: "no-cache",
      });

      if (!response.ok) {
        throw new Error("Failed to delete user. " + response.statusText);
      }

      sessionStorage.removeItem('userId');
      navigate("/Login");
      window.location.reload();
    } catch (error) {
      console.error("Error deleting user account:", error);
    }
  };

  return (
    <div className="form_container">
      <h2>
        Questa operazione è irreversibile e tutti i tuoi dati andranno persi.
      </h2>
      <img src={sessoMaschile} alt="Icona maschio" className="imgMaschio" />
      <button className="delete-button" onClick={handleDelete}>
        Elimina
      </button>
      {/* <FontAwesomeIcon icon={fa}>Elimina Il Tuo account</FontAwesomeIcon> */}
      {/* Aggiungi il contenuto specifico per la rimozione del profilo */}
    </div>
  );
};

export default EliminaUtente;
