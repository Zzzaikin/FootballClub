import React, { useState } from 'react';
import BootstrapNavbarBrand from './BootstrapNavbarBrand';

function LeftNav() {
    const [redirect, setRedirect] = useState();

    return (
        <div>
            <div class="nav flex-column nav-pills" id="v-pills-tab" role="tablist" aria-orientation="vertical">
                <BootstrapNavbarBrand label="Игроки" id="PlayersSection" isNavItemActive={true} goToSection={setRedirect} />
                <BootstrapNavbarBrand label="Тренеры" id="CoachesSection" isNavItemActive={false} goToSection={setRedirect}/>
                <BootstrapNavbarBrand label="Менеджеры игроков" id="PlayerManagersSection" isNavItemActive={false} goToSection={setRedirect}/>
                <BootstrapNavbarBrand label="Взыскания" id="EmployeeRecoveriesSection" isNavItemActive={false} goToSection={setRedirect}/>
                <BootstrapNavbarBrand label="Матчи" id="MatchesSection" isNavItemActive={false} goToSection={setRedirect}/>
                <BootstrapNavbarBrand label="Дисквалификации" id="DisqualificationsSection" isNavItemActive={false} goToSection={setRedirect}/>
            </div>
            {redirect}
        </div>
    );
}

export default LeftNav;