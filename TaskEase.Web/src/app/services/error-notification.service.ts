import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class ErrorNotificationService {
    showError = false;
    errorMessage = '';

    showErrorNotification(message: string) {
        this.showError = true;
        this.errorMessage = message;
        setTimeout(() => {
            this.hideErrorNotification();
        }, 3000);
    }

    hideErrorNotification() {
        this.showError = false;
        this.errorMessage = '';
    }
}
