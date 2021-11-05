import React, { useState, useEffect } from 'react';
import axios from "axios";
import { Table } from 'react-bootstrap';
import SlettOrdre from './SlettOrdre';
import UpdateOrdre from './OppdatereOrdre';

export default function Ordre() {
    const [bestillinger, setBestilinger] = useState([]);

    useEffect(() => {
        async function fetchSteder() {
            console.log('Fetch...');
            await axios.get('Kunde/GetAlleOrdre')
                .then(function (response) {
                    // handle success
                    console.log(response.data);
                    setBestilinger(response.data);
                })
                .catch(function (error) {
                    // handle error
                    console.log(error);
                })
                .then(function () {
                    // always executed
                });
        }
        fetchSteder();
    }, [])

    /*

    const lagNyReise = (nyReise) => {
        console.log(nyReise);
        setReiser([...reiser, nyReise]);
    }
    */

    const deleteOrdre = (id) => {
        setBestilinger(bestillinger.filter(ordre => ordre.oId !== id))
    }

    const oppdatereOrdre = (oppdatertOrdre) => {
        const newList = bestillinger.map((item) => {
            if (item.oId === oppdatertOrdre.oId) {
                return oppdatertOrdre;
            }
            return item;
        });

        setBestilinger(newList);
    }

    return (
        <div>

            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>#id</th>
                        <th>Antall Barn</th>
                        <th>Antall Studenter</th>
                        <th>Antall Voksne</th>
                        <th>Kunde Id</th>
                        <th>Fornavn</th>
                        <th>Etternavn</th>
                        <th>Reise Id</th>
                        <th>Fra</th>
                        <th>Til</th>
                        <th>Dato</th>
                        <th>Tid</th>
                        <th>Total Pris</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        bestillinger.length > 0 && bestillinger.map((ordre) => (
                            <tr>
                                <th scope="row">{ordre.oId}</th>
                                <td>{ordre.antallBarn}</td>
                                <td>{ordre.antallStudent}</td>
                                <td>{ordre.antallVoksne}</td>
                                <td>{ordre.kunder.kId}</td>
                                <td>{ordre.kunder.fornavn}</td>
                                <td>{ordre.kunder.etternavn}</td>
                                <td>{ordre.reiser.rId}</td>
                                <td>{ordre.reiser.fraSted.stedsNavn}</td>
                                <td>{ordre.reiser.tilSted.stedsNavn}</td>
                                <td>{ordre.reiser.dato}</td>
                                <td>{ordre.reiser.tid}</td>
                                <td>{ordre.totalPris}</td>
                                <td><SlettOrdre id={ordre.oId} deleteOrdre={deleteOrdre} /></td>
                                <td><UpdateOrdre ordre={ordre} oppdatereOrdre={oppdatereOrdre} /></td>
                            </tr>
                        ))
                    }
                </tbody>
            </Table>

        </div>
    )
}