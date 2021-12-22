import React from 'react';
import { Redirect, Route } from 'react-router-dom';
import CardPage from './CardPage';
import EmployeeRecoveriesMiniDashboard from './EmployeeRecoveriesMiniDashboard';
import MatchesMiniDashboard from './MatchesMiniDashboard';
import Section from './Section';
import EntityContent from './EntityContent';

function Content() {
    return (
        <div class="tab-content" id="v-pills-tabContent">
            <Route path='/PlayersSection' component={Section} />
            <Route path='/CoachesSection' component={Section} />
            <Route path='/PlayerManagersSection' component={Section} />
            <Route path='/EmployeeRecoveriesSection' render={() =>
                <Section miniDashboard={<EmployeeRecoveriesMiniDashboard />} match={{ path: "/EmployeeRecoveriesSection"}} />} /> 
            <Route path='/MatchesSection' render={() =>
                <Section miniDashboard={<MatchesMiniDashboard />} match={{ path: "/MatchesSection"}} />} />
            <Route path='/DisqualificationsSection' component={Section} />

            <Route path='/PlayersCardPage' render={() => <CardPage content={<EntityContent />} />} />
            <Route path='/CoachesCardPage' render={() => <CardPage content={<EntityContent />} />} />
            <Route path='/PlayerManagersCardPage' render={() => <CardPage content={<EntityContent />} />} />
            <Route path='/EmployeeRecoveriesCardPage' render={() => <CardPage content={<EntityContent />} />} />
            <Route path='/MatchesCardPage' render={() => <CardPage content={<EntityContent />} />} />
            <Route path='/DisqualificationsCardPage' render={() => <CardPage content={<EntityContent />} />} />
            <Redirect from={"/"} to={"/PlayersSection"} />
        </div>
    );
}

export default Content;
