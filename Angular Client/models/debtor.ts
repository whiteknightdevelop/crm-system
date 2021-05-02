export interface Debtor {
  debtId: number;
  ownerId: number;
  firstName: string;
  lastName: string;
  phone: string;
  debtAmountSum: number;
  paidAmountSum: number;
  totalAmount: number;
  debtDate: Date;
}

export class DebtorEntity implements Debtor{
  constructor() {
    this.debtId = 0;
    this.ownerId = 0;
    this.firstName = '';
    this.lastName = '';
    this.phone = '';
    this.debtAmountSum = 0;
    this.paidAmountSum = 0;
    this.totalAmount = 0;
    this.debtDate = new Date();
  }

  debtId: number;
  ownerId: number;
  firstName: string;
  lastName: string;
  phone: string;
  debtAmountSum: number;
  paidAmountSum: number;
  totalAmount: number;
  debtDate: Date;
}
