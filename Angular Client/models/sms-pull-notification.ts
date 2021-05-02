export interface SmsPullNotification {
  customerMessageId: number;
  customerParam: string;
  network: string;
  notificationDate: string;
  phoneNumber: string;
  segmentsNumber: number;
  senderNumber: string;
  sentMessage: string;
  status: number;
  statusDescription: string;
  type: string;
}

export class SmsPullNotificationEntity implements SmsPullNotification{
  constructor() {
    this.customerMessageId = 0;
    this.customerParam = '';
    this.network = '';
    this.notificationDate = '';
    this.phoneNumber = '';
    this.segmentsNumber = 0;
    this.senderNumber = '';
    this.sentMessage = '';
    this.status = 0;
    this.statusDescription = '';
    this.type = '';
  }
  customerMessageId: number;
  customerParam: string;
  network: string;
  notificationDate: string;
  phoneNumber: string;
  segmentsNumber: number;
  senderNumber: string;
  sentMessage: string;
  status: number;
  statusDescription: string;
  type: string;
}
