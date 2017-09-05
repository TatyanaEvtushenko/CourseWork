import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { ConfirmationForm } from '../viewmodels/confirmationform';

@Injectable()
export class UnconfirmedUserService extends BaseService {
    confirmAccount(confirmationForm: ConfirmationForm) {
        return this.requestPost("api/UnconfirmedUser/ConfirmAccount", confirmationForm);
    }
}