import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CreateUserComponent } from './components/create-user/create-user.component';

export const routes: Routes = [
     { path: '', component: HomeComponent },
     { path: 'users', component: CreateUserComponent },
];
