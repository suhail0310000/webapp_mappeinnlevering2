import React, { useState, useEffect } from 'react';
import axios from "axios";
import { Table } from 'reactstrap';
import { Tabs, Tab } from 'react-bootstrap';
import Remove from './Delete';
import Update from './Update';
import NyReise from './Create';
import { Redirect } from 'react-router-dom'
import Sted from './Sted/Sted';
import Kunde from './Kunde/Kunde';
import Ordre from './Ordre/Ordre';
import Bruker from './Bruker';

const stylesSelect = {
    padding: 15,
    marginBottom: 10,
    backgroundColor: '#eee',
    fontSize: 16
}

export default function Admin(props) {
    const [reiser, setReiser] = useState([]);
    const [steder, setSteder] = useState([]);
    const [option, setOption] = useState('reise');
    const [key, setKey] = useState(1);
    const [redirectToReferrer, setRedirectToReferrer] = useState(false)

    useEffect(() => {
        async function checkSession() {
            console.log('Fetch...');
            await axios.get('Kunde/ValidSession')
                .then(function (response) {
                    // handle success
                    console.log(response)
                    if (response.data === false) {
                        setRedirectToReferrer(true)
                    }
                })
                .catch(function (error) {
                    // handle error
                    console.log(error);
                })
                .then(function () {
                    // always executed
                });
        }
        async function fetchReiser() {
            console.log('Fetch...');
            await axios.get('Kunde/GetAlleReiser')
                .then(function (response) {
                    // handle success
                    const allReiser = response.data;
                    setReiser(allReiser);
                })
                .catch(function (error) {
                    // handle error
                    console.log(error);
                })
                .then(function () {
                    // always executed
                });
        }
        async function fetchSteder() {
            await axios.get('Kunde/GetAllSteder')
                .then(function (response) {
                    // handle success
                    let alleSteder = [];
                    
                    response.data.forEach((sted) => {
                        alleSteder.push(sted.stedsNavn);
                    })
                    setSteder(alleSteder);
                })
                .catch(function (error) {
                    // handle error
                    console.log(error);
                })
                .then(function () {
                    // always executed
                });
        }
        checkSession()
        fetchReiser();
        fetchSteder();
    }, [])

    const handleSelect = event => {
        let value = event.target.value;
        console.log(value);
        setOption(value);
    }

    const deleteReise = (id) => {
        setReiser(reiser.filter(reise => reise.rId !== id))
    }

    const oppdatereReise = (oppdatertReise) => {
        const newList = reiser.map((item) => {
            if (item.rId === oppdatertReise.rId) {
                return oppdatertReise;
            }
            return item;
        });

        setReiser(newList);
    }

    const lagNyReise = (nyReise) => {
        console.log(nyReise);
        setReiser([...reiser, nyReise]);
    }

    const { from } = props.location.state || {
        from: {
            pathname: '/logginn'
        }
    }

    if (redirectToReferrer) {
        return (<Redirect to={from} />)
    }

    return (
        <div style={{ padding: '10px 40px 10px 40px' }}>
            <h1 style={{ textAlign: 'center' }}>Admin</h1>
            {steder.length > 0 && <>
                <div style={{ display: 'flex', justifyContent: 'flex-start', cursor: 'pointer', alignItems: 'center' }}>
                    <h4 style={{ marginRight: 10 }}>Velg tabell: </h4>
                    <select style={stylesSelect} onChange={handleSelect} value={option} >
                        <option value="reise">Reiser</option>
                        <option value="kunde">Kunder</option>
                        <option value="ordre">Ordre</option>
                        <option value="sted">Steder</option>
                        <option value="bruker">Bruker</option>
                    </select>
                </div>

                {option === "reise" && <>
                    <NyReise destinasjoner={steder} lagNyReise={lagNyReise} />
                    <Tabs
                        id="controlled-tab-example"
                        activeKey={key}
                        onSelect={(k) => setKey(k)}
                        className="mt-4 mb-2"
                    >
                        {
                            steder.map((sted, index) => (
                                <Tab eventKey={index + 1} title={sted}>
                                    <h1>{sted}</h1>
                                    <Table striped>
                                        <thead>
                                            <tr>
                                                <th>#id</th>
                                                <th>Fra</th>
                                                <th>Dato</th>
                                                <th>Tid</th>
                                                <th>Pris barn</th>
                                                <th>Pris Student</th>
                                                <th>Pris Voksen</th>
                                                <th>Til</th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            {reiser.map((reise) => {
                                                if (sted === reise.fraSted.stedsNavn) {
                                                    return (
                                                        <tr>
                                                            <th scope="row">{reise.rId}</th>
                                                            <td>{reise.fraSted.stedsNavn}</td>
                                                            <td>{reise.dato}</td>
                                                            <td>{reise.tid}</td>
                                                            <td>{reise.prisBarn}</td>
                                                            <td>{reise.prisStudent}</td>
                                                            <td>{reise.prisVoksen}</td>
                                                            <td>{reise.tilSted.stedsNavn}</td>
                                                            <td><Remove id={reise.rId} deleteReise={deleteReise} /></td>
                                                            <td><Update reise={reise} destinasjoner={steder} oppdatereReise={oppdatereReise} /></td>
                                                        </tr>
                                                    )
                                                }
                                            })}
                                        </tbody>
                                    </Table>
                                </Tab>
                            ))
                        }
                    </Tabs>
                    </>
                }

                {option === "sted" && 
                    <Sted/>
                }

                {option === "kunde" &&
                    <Kunde />
                }

                {option === "ordre" &&
                    <Ordre/>
                }

                {option === "bruker" &&
                    <Bruker />
                }
            </>
            }
        </div>
    )
}