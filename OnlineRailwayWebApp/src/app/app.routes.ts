import { Routes } from '@angular/router';
import { RailwayHomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { SearchResultsComponent } from './components/search/search.component';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full'
    },
    {
        path: 'home',
        component: RailwayHomeComponent
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'search',
        component: SearchResultsComponent
    }
];
