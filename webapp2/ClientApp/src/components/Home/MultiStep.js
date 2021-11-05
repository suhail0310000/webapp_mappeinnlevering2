import React, { useState, useEffect } from 'react';
import './home.css';
import axios from "axios";
import { Button, Form, Modal } from 'react-bootstrap';

const styleInput = {
    width: '100%',
    padding: '6px 3px',
    display: 'inline-block',
    border: '1px solid #ccc',
    borderRadius: '4px',
    boxSizing: 'border-box',
}

const styleDisplay = {
    padding: 10,
    cursor: 'pointer',
    background: '#eee',
    margin: 5,
}

export default function Multi() {
    const [steder, setSteder] = useState([]);
    const [reiser, setReiser] = useState([]);
    const [avgang, setAvgang] = useState('');
    const [dato, setDato] = useState('');
    const [tid, setTid] = useState('');
    const [rId, setrId] = useState('');
    const [priser, setPriser] = useState({
        prisBarn: '',
        prisStudent: '',
        prisVoksen: '',
    })
    const [values, setValues] = useState({
        fornavn: '',
        etternavn: ''
    })
    const [destinasjon, setDestinasjon] = useState([]);
    const [avreiseDato, setAvreiseDato] = useState([]);
    const [tider, setTider] = useState([]);
    const [step, setStep] = useState(1);

    useEffect(() => {
        async function fetchSteder() {
            console.log('Fetch...');
            await axios.get('Kunde/GetAlleReiser')
                .then(function (response) {
                    // handle success
                    const hashset = new Set()
                    const allReiser = response.data;
                    let uniqueReiser = [];

                    allReiser.forEach((reise) => {
                        if (!hashset.has(reise.fraSted.stedsNavn)) {
                            hashset.add(reise.fraSted.stedsNavn);
                            uniqueReiser.push(reise.fraSted.stedsNavn);
                        }
                    })
                    console.log(uniqueReiser);
                    console.log(allReiser);
                    setSteder(uniqueReiser);
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
        fetchSteder();
    }, [])

    const nextStep = () => {
        if (step === 5) {
            if (priser.prisBarn < 1 || priser.prisVoksen < 1 || priser.prisStudent < 1) {
                return;
            }
        }
        setStep(step + 1);
    }

    const prevStep = () => {
        if (step === 0) {
            return;
        }
        setStep(step - 1);
    }
    console.log(step);

    const populateDestinasjoner = async (avgang) => {
        //console.log(e.target.value);
        //let avgang = e.target.value;
        setAvgang(avgang);
        nextStep();
    }

    const populateAvReiseDato = async (destinasjon) => {
       // let destinasjon = e.target.value;
        setDestinasjon(destinasjon);
        const hashset = new Set()

        reiser.forEach((reise) => {
            if (avgang == reise.fraSted.stedsNavn && destinasjon === reise.tilSted.stedsNavn) {
                console.log(reise);
                if (!hashset.has(reise.dato)) {
                    hashset.add(reise.dato);
                }
            }
        })
        let datoer = Array.from(hashset);
        console.log(datoer);
        setAvreiseDato(datoer);
        nextStep();
    }

    const populateTid = (dato) => {
        setDato(dato);
        const hashset = new Set()

        reiser.forEach((reise) => {
            if (avgang == reise.fraSted.stedsNavn && destinasjon === reise.tilSted.stedsNavn && dato == reise.dato) {
                console.log(reise);
                if (!hashset.has(reise.tid)) {
                    hashset.add(reise.tid);
                }
            }
        })
        let tider = Array.from(hashset);
        console.log(tider);
        setTider(tider);
        nextStep();
    }

    const pris = (tid) => {
        setTid(tid)
        nextStep();
    }

    const handleTextfield = (type, name) => event => {
        let value = event.target.value;
        if (type === "pris") {
            let toNumber = false;
            if (name === "prisBarn" || name === "prisStudent" || name === "prisVoksen") {
                toNumber = true;
            }
            setPriser({ ...priser, [name]: toNumber ? Number(value) : value })
        }
        if (type === "text") {
            setValues({ ...values, [name]: value })
        }
    }

    const bestill = async () => {

        const ordre = {
            fraSted: avgang,
            tilSted: destinasjon,
            dato: dato,
            tid: tid,
            fornavn: values.fornavn,
            etternavn: values.etternavn,
            antallBarn: priser.prisBarn,
            antallStudent: priser.prisStudent,
            antallVoksne: priser.prisVoksen
        }
        console.log(ordre);

        await axios.post('Kunde/Lagre', ordre)
            .then(function (response) {
                // handle success
                console.log(response);
                if (response.data === "Bestillingen er Lagret") {
                    alert(response.data);
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

    const calculateTotal = () => {
        let total = 0;

    }


    return (
        <div style={{ height: '100vh', position: 'relative', backgroundImage: `url(${require("../../assets/cruise.jpeg")})`, backgroundSize: 'cover', backgroundPosition: 'center', paddingTop: 20 }}>

            <div style={{ padding: 20, margin: 'auto', width: 500, maxWidth: '100%', backgroundColor: '#fff'}}>
            {step === 1 &&
                <div className="form-group">
                    <h4>Avgang:</h4>
                    {steder.length > 0 && steder.map((sted) =>
                        <div onClick={() => populateDestinasjoner(sted)} style={styleDisplay}>
                            {sted}
                        </div>
                     )}
                    <div id="selectAvgang"></div>
                </div>
            }
                {step === 2 &&
                <div className="form-group">
                    <h4>Destinasjon:</h4>
                    {steder.map((destinasjon) => {
                        if (avgang !== destinasjon) {
                            return (
                                <div onClick={() => populateAvReiseDato(destinasjon)} style={styleDisplay}>
                                    {destinasjon}
                                </div>
                            )
                        }
                    })}
                </div>
            }

            {step === 3 &&
                    <div>
                    <h4>Velg dato:</h4>
                    {avreiseDato.length > 0 && avreiseDato.map((dato) =>
                        <div onClick={() => populateTid(dato)} style={styleDisplay}>
                            {dato}
                        </div>
                    )}
                </div>
            }

            {step === 4 &&
                    <div>
                    <h4>Velg tid:</h4>
                    {tider.length > 0 && tider.map((tid) =>
                        <div onClick={() => pris(tid)} style={styleDisplay}>
                            {tid}
                        </div>
                    )}
                </div>
            }


            {step === 5 &&
                <>
                    <div className="mb-6">
                        <span className="form-label">Antall Barn</span>
                        <div className="form-control-sm-6">
                            <input style={styleInput} type="number" value={priser.prisBarn} onChange={handleTextfield('pris', 'prisBarn')} />
                            <div id="feilAntallBarn" style={{ color: "red" }}></div>
                        </div>
                    </div>
                    <div className="mg-6">
                        <span className="form-label">Antall Studenter</span>
                        <div className="form-control-sm-6">
                            <input style={styleInput} type="number" value={priser.prisStudent} onChange={handleTextfield('pris', 'prisStudent')} />
                            <div id="antallStudenterFeil" style={{ color: "red" }}></div>
                        </div>
                    </div>
                    <div>
                        <span className="form-label">Antall Voksne</span>

                        <div className="form-control-sm-6">
                            <input style={styleInput}  type="number" value={priser.prisVoksen} onChange={handleTextfield('pris', 'prisVoksen')} />
                            <div id="feilAntallVoksne" style={{ color: "red" }}></div>
                        </div>
                    </div>
                </>
            }

            {step === 6 &&
                <>
                    <div className="mb-6">
                        <span className="form-label">Fornavn</span>
                        <div className="form-control-sm-6">
                            <input style={styleInput} type="text" value={priser.fornavn} onChange={handleTextfield('text', 'fornavn')} />
                            <div id="feilAntallBarn" style={{ color: "red" }}></div>
                        </div>
                    </div>
                    <div className="mg-6">
                        <span className="form-label">Etternavn</span>
                        <div className="form-control-sm-6">
                            <input style={styleInput} type="text" value={values.etternavn} onChange={handleTextfield('text', 'etternavn')} />
                            <div id="antallStudenterFeil" style={{ color: "red" }}></div>
                        </div>
                    </div>
                </>
            }

            {step === 7 &&
                    <>
                        <h4>Orde:</h4>
                        <p><strong>Avgang:</strong> {avgang}</p>
                        <p><strong>Destinasjon:</strong> {destinasjon}</p>
                        <p><strong>Avreise dato:</strong> {dato}</p>
                        <p><strong>Tid:</strong> {tid} </p>
                        <p><strong>Antall barn:</strong> {priser.prisBarn}</p>
                        <p><strong>Antall voksne:</strong> {priser.prisVoksen}</p>
                        <p><strong>Antall studenter:</strong> {priser.prisStudent}</p>
                        <p><strong>Fornavn:</strong> {values.fornavn}</p>
                        <p><strong>Etternavn:</strong> {values.etternavn}</p>
                        {/*<p><strong>Total:</strong> `${priser.prisStudent}</p> */}
                    </>
            }

            {step !== 1 && <Button className="mt-1" variant="secondary" onClick={prevStep}>Forrige</Button> }
            {(step === 5 || step === 6)  && <Button className="mt-1"  variant="primary" onClick={nextStep}>Neste</Button> }
            {step === 7 && <Button className="mt-1" variant="primary"onClick={bestill}>Fullfør bestilling</Button>}
            </div>

        </div>        
     )
}