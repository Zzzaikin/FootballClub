import React from 'react';
import { Redirect, Route } from 'react-router-dom';
import EmployeeRecoveriesMiniDashboard from './EmployeeRecoveriesMiniDashboard';
import Section from './Section';

function Content() {
    return (
        <div class="tab-content" id="v-pills-tabContent">
            <Route path='/PlayersSection' component={Section} />
            <Route path='/CoachesSection' component={Section} />
            <Route path='/PlayerManagersSection' component={Section} />
            <Route path='/EmployeeRecoveriesSection' render={() =>
                <Section miniDashboard={<EmployeeRecoveriesMiniDashboard />} match={{ path: "/EmployeeRecoveriesSection"}} />} /> 
            <Route path='/MatchesSection' component={Section} />
            <Route path='/DisqualificationsSection' component={Section} />
            <Redirect from={"/"} to={"/PlayersSection"} />
        </div>
    );
}

export default Content;
