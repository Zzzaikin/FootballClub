import React, { useState, useEffect } from 'react';
import { Redirect } from 'react-router-dom';

export default function BootstrapNavbarBrand(props) {
    const [isActive, setIsActive] = useState();

    useEffect(() => {
        setIsActive(props.isNavItemActive);
    }, []);

    function onNavItemClick(event) {
        const id = event.currentTarget.id;
        setIsActive(true);

        const redirectComponent = <Redirect from="/" to={`/${id}`} />;
        props.goToSection(redirectComponent);
    }

    return (
        <a
            className={props.isNavItemActive ? "nav-link active" : "nav-link"}
            onClick={(event => onNavItemClick(event))}
            data-toggle="pill"
            href={props.id}
            role="tab"
            aria-controls={`v-pills-${props.id}`}
            aria-selected={isActive}
            id={props.id}>{props.label}</a>
    );
}
