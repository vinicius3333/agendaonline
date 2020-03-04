import { Component, OnInit, TemplateRef } from '@angular/core';
import { AgendaService } from '../_services/Agenda.service';
import { Agenda } from '../_models/Agenda';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { FormGroup, Validators, FormBuilder, FormArray } from '@angular/forms';
import { defineLocale, BsLocaleService, ptBrLocale } from 'ngx-bootstrap';
import { ToastrService } from 'ngx-toastr';

defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-eventos',
  templateUrl: './agendamentos.component.html',
  styleUrls: ['./agendamentos.component.css']
})
export class AgendamentosComponent implements OnInit {

  dataEvento: string;
  agendamentosFiltrados: Agenda[];
  agendas: Agenda[];
  agenda: Agenda;
  modoSalvar = 'post';

  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  registerForm: FormGroup;
  bodyDeletarEvento = '';

  file: File;
  fileNameToUpdate: string;

  dataAtual: string;

  _filtroLista = '';

  constructor(
    private agendaService: AgendaService
    , private modalService: BsModalService
    , private fb: FormBuilder
    , private localeService: BsLocaleService
    , private toastr: ToastrService
  ) {
    this.localeService.use('pt-br');
  }

  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    this._filtroLista = value;
    this.agendamentosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.agendas;
  }

  // Sugestão do Aluno Kelvi Martins Ribeiro
  filtrarEventos(filtrarPor: string) {
    filtrarPor = filtrarPor.toLocaleLowerCase()
    return this.agendas.filter(agenda => {
      return agenda.nome.toLocaleLowerCase().includes(filtrarPor)
    })
  }

  // // Sugestão do Aluno Pablo Ferreira
  // filtrarClientes(filtrarPor: string): Cliente[] {
  //   filtrarPor = filtrarPor.toLocaleLowerCase();
  //   return this._cliente.filter(
  //     cliente => cliente.nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1
  //     || cliente.nomeDelegacia.toLocaleLowerCase().indexOf(filtrarPor) !== -1
  //     || cliente.status.toLocaleLowerCase().startsWith(filtrarPor)
  //   );
  // }

  editarEvento(agenda: Agenda, template: any) {
    this.modoSalvar = 'put';
    this.openModal(template);
    this.agenda = Object.assign({}, agenda);
    //this.fileNameToUpdate = agenda.imagemURL.toString();
    //this.agenda.imagemURL = '';
    this.registerForm.patchValue(this.agenda);
  }

  novoEvento(template: any) {
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  abrirModalhorarios(horario: any){
    horario.show();
  }

  excluirEvento(evento: Agenda, template: any) {
    this.openModal(template);
    this.agenda = evento;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir este agendamento: ${evento.nome}`;
  }

  confirmeDelete(template: any) {
    this.agendaService.deleteEvento(this.agenda.id).subscribe(
      () => {
        template.hide();
        this.getEventos();
        this.toastr.success('Deletado com Sucesso');
      }, error => {
        this.toastr.error('Erro ao tentar Deletar');
        console.log(error);
      }
    );
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  ngOnInit() {
    this.validation();
    this.getEventos();
  }

  // filtrarEventos(filtrarPor: string): Evento[] {
  //   filtrarPor = filtrarPor.toLocaleLowerCase();
  //   return this.eventos.filter(
  //     evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
  //   );
  // }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  validation() {
    this.registerForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      email: ['', Validators.required],
      dataHora: ['', Validators.required],
      celular: ['', Validators.required],
      userId: ['', Validators.required],
    });
  }

  onFileChange(agen) {
    const reader = new FileReader();

    if (agen.target.files && agen.target.files.length) {
      this.file = agen.target.files;
      console.log(this.file);
    }
  }

  // uploadImagem() {
  //   if (this.modoSalvar === 'post') {
  //     const nomeArquivo = this.agenda.imagemURL.split('\\', 3);
  //     this.agenda.imagemURL = nomeArquivo[2];

  //     this.agendaService.postUpload(this.file, nomeArquivo[2])
  //       .subscribe(
  //         () => {
  //           this.dataAtual = new Date().getMilliseconds().toString();
  //           this.getEventos();
  //         }
  //       );
  //   } else {
  //     this.agenda.imagemURL = this.fileNameToUpdate;
  //     this.agendaService.postUpload(this.file, this.fileNameToUpdate)
  //       .subscribe(
  //         () => {
  //           this.dataAtual = new Date().getMilliseconds().toString();
  //           this.getEventos();
  //         }
  //       );
  //   }
  // }

  salvarAlteracao(template: any) {
    if (this.registerForm.valid) {
      if (this.modoSalvar === 'post') {
        this.agenda = Object.assign({}, this.registerForm.value);

        //this.uploadImagem();

        this.agendaService.postEvento(this.agenda).subscribe(
          (novoAgendamento: Agenda) => {
            template.hide();
            this.getEventos();
            this.toastr.success('Inserido com Sucesso!');
          }, error => {
            this.toastr.error(`Erro ao Inserir: ${error}`);
          }
        );
      } else {
        this.agenda = Object.assign({ id: this.agenda.id }, this.registerForm.value);

        //this.uploadImagem();

        this.agendaService.putEvento(this.agenda).subscribe(
          () => {
            template.hide();
            this.getEventos();
            this.toastr.success('Editado com Sucesso!');
          }, error => {
            this.toastr.error(`Erro ao Editar: ${error}`);
          }
        );
      }
    }
  }

  getEventos() {
    this.dataAtual = new Date().getMilliseconds().toString();

    this.agendaService.getAllEvento().subscribe(
      (_agenda: Agenda[]) => {
        this.agendas = _agenda;
        this.agendamentosFiltrados = this.agendas;
        console.log(this.agendas);
      }, error => {
        this.toastr.error(`Erro ao tentar Carregar eventos: ${error}`);
      });
  }

}
