import React, { useState, useEffect } from 'react';
import axios from "axios";
import { Table } from 'react-bootstrap';
import CreateKunde from './CreateKunde';
import SlettKunde from './SlettKunde'
import UpdateKunde from './OppdatereKunde';

export default function Kunde() {
    const [kunder, setKunder] = useState([]);

    useEffect(() => {
        async function fetchSteder() {
            console.log('Fetch...');
            await axios.get('Kunde/GetAlleKunder')
                .then(function (response) {
                    // handle success
                    console.log(response.data);
                    setKunder(response.data);
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
    const oppdatereReise = (oppdatertReise) => {
        const newList = reiser.map((item) => {
            if (item.rId === oppdatertReise.rId) {
                return oppdatertReise;
            }
            return item;
        });

        setReiser(newList);
    }
    */

    const createKunde = (nyKunde) => {
        console.log(nyKunde);
        setKunder([...kunder, nyKunde]);
    }

    const deleteKunde = (id) => {
        setKunder(kunder.filter(kunde => kunde.kId !== id))
    }

    const oppdatereKunde = (oppdatertKunde) => {
        const newList = kunder.map((item) => {
            if (item.kId === oppdatertKunde.kId) {
                return oppdatertKunde;
            }
            return item;
        });

        setKunder(newList);
    }

    return (
        <div>
            <CreateKunde createKunde={createKunde} />
            <Table striped bordered hover style={{ maxWidth: '50%' }}>
                <thead>
                    <tr>
                        <th>#id</th>
                        <th>Fornavn</th>
                        <th>Etternavn</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        kunder.length > 0 && kunder.map((kunde) => (
                            <tr>
                                <th scope="row">{kunde.kId}</th>
                                <td>{kunde.fornavn}</td>
                                <td>{kunde.etternavn}</td>
                                <td width={"10%"}><SlettKunde id={kunde.kId} deleteKunde={deleteKunde} /></td>
                                <td width={"10%"}><UpdateKunde kunde={kunde} oppdatereKunde={oppdatereKunde} /></td>
                            </tr>
                        ))
                    }
                </tbody>
            </Table>

        </div>
    )
}