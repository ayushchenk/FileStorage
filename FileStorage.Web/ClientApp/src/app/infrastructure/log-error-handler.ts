import { ErrorHandler } from '@angular/core';

export class LogErrorHandler implements ErrorHandler {
    handleError(error) {
        console.log(error);
    }
  }