import { Component, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AccountService } from '../account/shared/account.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  modalRef: BsModalRef;

  constructor(
    private modalService: BsModalService,
    private accountService: AccountService,
    private router: Router
  ) { }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  openModal(template: TemplateRef<any>, id: string) {
    this.modalRef = this.modalService.show(template, { class: 'modal-sm', animated: false });
   }
 
   decline(): void {
     this.modalRef.hide();
   }

  onSubmit() {
    try {
      this.modalRef.hide();
      this.accountService.logout();
    } catch (error) {
      console.error(error);
    }
  }
}
