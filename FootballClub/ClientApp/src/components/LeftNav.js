import React from 'react';
import { Link } from 'react-router-dom';
import { NavbarBrand } from 'reactstrap';

function LeftNav() {
    return (
        <div>
            <NavbarBrand tag={Link} to="/PlayersSection">Игроки</NavbarBrand>
            <NavbarBrand tag={Link} to="/CoachesSection">Тренеры</NavbarBrand>
            <NavbarBrand tag={Link} to="/MenegersSection">Менеджеры игроков</NavbarBrand>
            <NavbarBrand tag={Link} to="/RecoverySection">Взыскания</NavbarBrand>
            <NavbarBrand tag={Link} to="/MatchesSection">Матчи</NavbarBrand>
            <NavbarBrand tag={Link} to="/DisqualificationsSection">Дисквалификации</NavbarBrand>
        </div>
    );
}

export default LeftNav;