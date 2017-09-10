import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { SentMessage } from "../viewmodels/sentmessage";

@Injectable()
export class MessageSenderService extends BaseService {
	sendMessage(messages: SentMessage[]) {
		return this.requestPost("api/Message/Send", messages);
	}
}