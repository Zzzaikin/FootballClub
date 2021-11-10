import React from 'react';
import { BrowserRouter, Route } from 'react-router-dom';
import Section from './Section';

function Content() {
    return (
        <div class="tab-content" id="v-pills-tabContent">
            <Route path='/PlayersSection' component={Section} />
            <Route path='/CoachesSection' component={Section} />
            <Route path='/MenegersSection' component={Section} />
            <Route path='/RecoverySection' component={Section} />
            <Route path='/MatchesSection' component={Section} />
            <Route path='/DisqualificationsSection' component={Section} />
        </div>
    );
}

export default Content;
