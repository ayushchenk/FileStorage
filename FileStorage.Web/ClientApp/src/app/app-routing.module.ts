import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from 'src/modules/account/login/login.component';
import { AdminComponent } from 'src/modules/admin/admin.component';
import { AdminGuardService } from 'src/modules/account/service/admin-guard.service';
import { CategoryHomeComponent } from 'src/modules/admin/category/category-home/category-home.component';
import { CreateCategoryComponent } from 'src/modules/admin/category/create-category/create-category.component';
import { EditCategoryComponent } from 'src/modules/admin/category/edit-category/edit-category.component';
import { UserComponent } from 'src/modules/user/user.component';
import { UserGuardService } from 'src/modules/account/service/user-guard.service';
import { FileHomeComponent } from 'src/modules/user/file/file-home/file-home.component';
import { TokenManager } from 'src/modules/account/service/token-manager';
import { RegisterComponent } from 'src/modules/account/register/register.component';

const routes: Routes = [
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  {
    path: 'admin', component: AdminComponent, canActivate: [AdminGuardService], children: [
      {
        path: 'categories', component: CategoryHomeComponent, canActivate: [AdminGuardService], children: [
          { path: 'create', component: CreateCategoryComponent, canActivate: [AdminGuardService], },
          { path: 'edit/:id', component: EditCategoryComponent, canActivate: [AdminGuardService], },
        ]
      }
    ]
  },
  {
    path: 'user', component: UserComponent, canActivate: [UserGuardService], children: [
      {
        path: "files", component: FileHomeComponent, canActivate: [UserGuardService], children: [
        ]
      }
    ]
  },
  { path: '**', redirectTo: 'user/files' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    onSameUrlNavigation: 'reload'
  })],
  exports: [RouterModule],
  providers: [TokenManager, AdminGuardService, UserGuardService]
})
export class AppRoutingModule { }
