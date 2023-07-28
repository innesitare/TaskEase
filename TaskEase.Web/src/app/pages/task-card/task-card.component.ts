import { Component } from '@angular/core'
import { Title } from '@angular/platform-browser'

@Component({
  selector: 'task-card',
  templateUrl: 'task-card.component.html',
  styleUrls: ['task-card.component.css'],
})
export class TaskCardComponent {
  constructor(private title: Title) {
    this.title.setTitle('TaskEase')
  }
}
