import React, { useState, useEffect } from 'react';
import axios from "axios";
import { Button, Modal, Form } from 'react-bootstrap';
import { BsPencilSquare } from 'react-icons/bs';
import { validereReise } from '../utils/validering.js';
/*
class Reise {
    constructor(rid, fraSted, tilSted, dato, tid, prisBarn, prisStudent, prisVoksen) {
        this.RId = rid;
        this.FraSted =  new Sted(fraSted);
        this.TilSted = new Sted(tilSted);
        this.Dato = dato;
        this.Tid = tid;
        this.PrisBarn = prisBarn;
        this.PrisStudent = prisStudent;
        this.PrisVoksen = prisVoksen;
    }
}
class Sted {
    constructor(sted) {
        this.stedsNavn = sted;
    }
}
*/

export default function Update(props) {
    const [show, setShow] = useState(false);
    const [values, setValues] = useState(props.reise);
    const [avgang, setAvgang] = useState(props.reise.fraSted.stedsNavn);
    const [destinasjon, setDestinasjon] = useState(props.reise.tilSted.stedsNavn);
    const [errors, setErrors] = useState({
        fraSted: {
            isValid: true,
            message: ""
        },
        tilSted: {
            isValid: true,
            message: ""
        },
        dato: {
            isValid: true,
            message: ""
        },
        tid: {
            isValid: true,
            message: ""
        },
        prisBarn: {
            isValid: true,
            message: ""
        },
        prisStudent: {
            isValid: true,
            message: ""
        },
        prisVoksen: {
            isValid: true,
            message: ""
        }
    })

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const handleSelect = name => event => {
        let value = event.target.value;
        if (name === "destinasjon") {
            setDestinasjon(value);
        } else if (name === "avgang") {
            setAvgang(value);
        }
    }

    const handleTextfield = name => event => {
        let value = event.target.value;
        let toNumber = false;
        if (name === "prisBarn" || name === "prisStudent" || name === "prisVoksen") {
            toNumber = true;
        }
        setValues({ ...values, [name]: toNumber ? Number(value) : value })
    }

    const updateReise = async () => {
        console.log(values);
        
        let reiseValidering = {
            fraSted: avgang,
            tilSted: destinasjon,
            dato: values.dato,
            tid: values.tid,
            prisBarn: values.prisBarn,
            prisStudent: values.prisStudent,
            prisVoksen: values.prisVoksen
        }

        let validation = validereReise(reiseValidering, errors);
        setErrors(validation.errors);

        console.log(validation);
        // Stop if one input is not validated 
        if (!validation.isValid) {
            return;
        }

        const oppdatertReise = {
            rId: values.rId,
            fraSted: { stedsNavn: avgang },
            tilSted: { stedsNavn: destinasjon },
            dato: values.dato,
            tid: values.tid,
            prisBarn: values.prisBarn,
            prisStudent: values.prisStudent,
            prisVoksen: values.prisVoksen
        }
        console.log(oppdatertReise);
        
        await axios.put('Kunde/OppdatereReise', oppdatertReise)
            .then(function (response) {
                // handle success
                console.log(response);
                if (response.data) {
                    props.oppdatereReise(oppdatertReise)
                    handleClose();
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
 

    return (
        <div>
            <Button variant="primary" onClick={handleShow}>
                <BsPencilSquare />
            </Button>
            <Modal animation={false} show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Oppdatere reise</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        {props.destinasjoner.length > 0 &&
                            <>
                                <Form.Group className="mb-3">
                                    <Form.Label>Avgang</Form.Label>
                                    {props.destinasjoner.length > 0 &&
                                        <select onChange={handleSelect("avgang")} value={avgang}>
                                            <option value="">Velg...</option>
                                            {props.destinasjoner.map((avgang) =>
                                                <option value={avgang}>{avgang}</option>
                                            )}
                                        </select>
                                    }
                                    <div style={{ color: 'red' }}>{errors.fraSted.isValid === false ? 'Avgang ' + errors.fraSted.message : ''} </div>
                                </Form.Group>

                                <Form.Group className="mb-3">
                                    <Form.Label>Destinasjoner</Form.Label>
                                    {avgang !== "" &&
                                        <select onChange={handleSelect("destinasjon")} value={destinasjon} >
                                            <option value="">Velg...</option>
                                            {props.destinasjoner.map((destinasjon) => {
                                                if (avgang !== destinasjon) {
                                                    return (
                                                        <option>{destinasjon}</option>
                                                    )
                                                }
                                            })}
                                        </select>
                                    }
                                    <div style={{ color: 'red' }}>{errors.tilSted.isValid === false ? 'Destinasjon ' + errors.tilSted.message : ''} </div>
                                </Form.Group>
                            </>
                        }
                        <Form.Group className="mb-3">
                            <Form.Label>Dato</Form.Label>
                            <Form.Control type="text" placeholder="Dato" value={values.dato} onChange={handleTextfield('dato')} />
                            <div style={{ color: 'red' }}>{errors.dato.isValid === false ? 'Dato ' + errors.dato.message : ''} </div>
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Tid</Form.Label>
                            <Form.Control type="text" placeholder="Tid" value={values.tid} onChange={handleTextfield('tid')} />
                            <div style={{ color: 'red' }}>{errors.tid.isValid === false ? 'Tid ' + errors.tid.message : ''} </div>
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Pris barn</Form.Label>
                            <Form.Control type="number" placeholder="Pris barn" value={values.prisBarn} onChange={handleTextfield('prisBarn')} />
                            <div style={{ color: 'red' }}>{errors.prisBarn.isValid === false ? 'Barn pris ' + errors.prisBarn.message : ''} </div>
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Pris voksen</Form.Label>
                            <Form.Control type="number" placeholder="Pris voksen" value={values.prisVoksen} onChange={handleTextfield('prisVoksen')} />
                            <div style={{ color: 'red' }}>{errors.prisVoksen.isValid === false ? 'Student pris ' + errors.prisVoksen.message : ''} </div>
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Pris student</Form.Label>
                            <Form.Control type="number" placeholder="Pris student" value={values.prisStudent} onChange={handleTextfield('prisStudent')} />
                            <div style={{ color: 'red' }}>{errors.prisStudent.isValid === false ? 'Student pris ' + errors.prisStudent.message : ''} </div>
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Nei
                    </Button>
                    <Button variant="primary" onClick={updateReise}>
                        Ja
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    )
}