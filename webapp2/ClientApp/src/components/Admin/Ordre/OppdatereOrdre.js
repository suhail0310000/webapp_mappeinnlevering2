import React, { useState } from 'react';
import axios from "axios";
import { Button, Modal, Form } from 'react-bootstrap';
import { BsPencilSquare } from 'react-icons/bs';
import { validereReise } from '../../utils/validering.js';

export default function UpdateOrdre(props) {
    const [show, setShow] = useState(false);
    const [values, setValues] = useState({
        antallBarn: props.ordre.antallBarn,
        antallStudent: props.ordre.antallStudent,
        antallVoksne: props.ordre.antallVoksne,
        //totalPris: props.ordre.totalPris,
        fornavn: props.ordre.kunder.fornavn,
        etternavn: props.ordre.kunder.etternavn,
        fraSted: props.ordre.reiser.fraSted.stedsNavn,
        tilSted: props.ordre.reiser.tilSted.stedsNavn,
        dato: props.ordre.reiser.dato,
        tid: props.ordre.reiser.tid,
        prisBarn: props.ordre.reiser.prisBarn,
        prisStudent: props.ordre.reiser.prisStudent,
        prisVoksen: props.ordre.reiser.prisVoksen

    });
    const [errors, setErrors] = useState({
        antallBarn: {
            isValid: true,
            message: ""
        },
        antallStudent: {
            isValid: true,
            message: ""
        },
        antallVoksne: {
            isValid: true,
            message: ""
        },
        fornavn: {
            isValid: true,
            message: ""
        },
        etternavn: {
            isValid: true,
            message: ""
        },
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

    const handleTextfield = name => event => {
        let value = event.target.value;
        let toNumber = false;
        if (name === "antallBarn" || name === "antallStudent" || name === "antallVoksne" || name === "prisBarn" || name === "prisStudent" || name === "prisVoksen") {
            toNumber = true;
        }
        setValues({ ...values, [name]: toNumber ? Number(value) : value })
    }

    const updateOrdre = async () => {
        console.log(values);

        let validation = validereReise(values, errors);
        setErrors(validation.errors);
        if (!validation.isValid) {
            return;
        }
        const oppdatertOrdre = {
            oId: props.ordre.oId,
            antallBarn: values.antallBarn,
            antallStudent: values.antallStudent,
            antallVoksne: values.antallVoksne,
            reiser: {
                rId: props.ordre.reiser.rId,
                fraSted: {
                    sId: props.ordre.reiser.fraSted.sId,
                    stedsNavn: values.fraSted
                },
                tilSted: {
                    sId: props.ordre.reiser.tilSted.sId,
                    stedsNavn: values.tilSted
                },
                dato: values.dato,
                tid: values.tid,
                prisBarn: values.prisBarn,
                prisStudent: values.prisStudent,
                prisVoksen: values.prisVoksen
            },
            kunder: {
                kId: props.ordre.kunder.kId,
                fornavn: values.fornavn,
                etternavn: values.etternavn,
            }
        }
        console.log(oppdatertOrdre);
        
        await axios.put('Kunde/EndreOrdre', oppdatertOrdre)
            .then(function (response) {
                // handle success
                console.log(response);
                if (response.data === "Ordre er nå oppdatert!") {
                    props.oppdatereOrdre(oppdatertOrdre)
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
                        <Form.Group className="mb-3">
                            <Form.Label>Antall barn</Form.Label>
                            <Form.Control type="text" placeholder="Antall barn" value={values.antallBarn} onChange={handleTextfield('antallBarn')} />
                            <div style={{ color: 'red' }}>{errors.antallBarn.isValid === false ? 'Antall barn ' + errors.antallBarn.message : ''} </div>
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Antall studenter</Form.Label>
                            <Form.Control type="text" placeholder="Antall studenter" value={values.antallStudent} onChange={handleTextfield('antallStudent')} />
                            <div style={{ color: 'red' }}>{errors.antallStudent.isValid === false ? 'Antall student ' + errors.antallStudent.message : ''} </div>
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Pris voksne</Form.Label>
                            <Form.Control type="text" placeholder="Antall voksne" value={values.antallVoksne} onChange={handleTextfield('antallVoksne')} />
                            <div style={{ color: 'red' }}>{errors.antallVoksne.isValid === false ? 'Antall voksne ' + errors.antallVoksne.message : ''} </div>
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Fornavn</Form.Label>
                            <Form.Control type="text" placeholder="Fornavn" value={values.fornavn} onChange={handleTextfield('fornavn')} />
                            <div style={{ color: 'red' }}>{errors.fornavn.isValid === false ? 'Fornavn ' + errors.fornavn.message : ''} </div>
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Etternavn</Form.Label>
                            <Form.Control type="text" placeholder="Etternavn" value={values.etternavn} onChange={handleTextfield('etternavn')} />
                            <div style={{ color: 'red' }}>{errors.etternavn.isValid === false ? 'Etternavn ' + errors.etternavn.message : ''} </div>
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Dato</Form.Label>
                            <Form.Control type="tex" placeholder="Dato" value={values.dato} onChange={handleTextfield('dato')} />
                            <div style={{ color: 'red' }}>{errors.dato.isValid === false ? 'Dato ' + errors.dato.message : ''} </div>
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Tid</Form.Label>
                            <Form.Control type="text" placeholder="Tid" value={values.tid} onChange={handleTextfield('tid')} />
                            <div style={{ color: 'red' }}>{errors.tid.isValid === false ? 'Tid ' + errors.tid.message : ''} </div>
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Pris barn</Form.Label>
                            <Form.Control type="text" placeholder="Pris barn" value={values.prisBarn} onChange={handleTextfield('prisBarn')} />
                            <div style={{ color: 'red' }}>{errors.prisBarn.isValid === false ? 'Barn pris ' + errors.prisBarn.message : ''} </div>
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Pris student</Form.Label>
                                    <Form.Control type="text" placeholder="Pris student" value={values.prisStudent} onChange={handleTextfield('prisStudent')} />
                                <div style={{ color: 'red' }}>{errors.prisStudent.isValid === false ? 'Student pris ' + errors.prisStudent.message : ''} </div>
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Pris voksen</Form.Label>
                            <Form.Control type="text" placeholder="Pris voksen" value={values.prisVoksen} onChange={handleTextfield('prisVoksen')} />
                            <div style={{ color: 'red' }}>{errors.prisVoksen.isValid === false ? 'Pris voksen ' + errors.prisVoksen.message : ''} </div>
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Nei
                    </Button>
                    <Button variant="primary" onClick={updateOrdre}>
                        Ja
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    )
}