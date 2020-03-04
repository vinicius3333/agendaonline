import { Component, OnInit, TemplateRef } from '@angular/core';
import { AgendaService } from '../_services/Agenda.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { User } from '../_models/User';
import { FormGroup, Validators, FormBuilder, FormArray } from '@angular/forms';
import { defineLocale, BsLocaleService, ptBrLocale } from 'ngx-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../_services/User.service';

defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-eventos',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  dataUsers: string;
  usuariosFiltrados: User[];
  usuarios: User[];
  usuario: User;

  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  registerForm: FormGroup;

  file: File;
  fileNameToUpdate: string;

  dataAtual: string;

  _filtroLista = '';

  constructor(
    private userService: UserService
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
    this.usuariosFiltrados = this.filtroLista ? this.filtrarUsuarios(this.filtroLista) : this.usuarios;
  }

  // Sugestão do Aluno Kelvi Martins Ribeiro
  filtrarUsuarios(filtrarPor: string) {
    filtrarPor = filtrarPor.toLocaleLowerCase()
    return this.usuarios.filter(usuario => {
      return usuario.company.toLocaleLowerCase().includes(filtrarPor)
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

  ngOnInit() {
    this.validation();
    this.getUsuarios();
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
  
  getUsuarios() {
    this.dataAtual = new Date().getMilliseconds().toString();
    this.userService.listaUsuariosPorAgenda().subscribe(
      (_usuario: User[]) => {
        this.usuarios = _usuario;
        this.usuariosFiltrados = this.usuarios;
        console.log(this.usuarios);
      }, error => {
        this.toastr.error(`Erro ao tentar Carregar eventos: ${error}`);
      });
  }

}
