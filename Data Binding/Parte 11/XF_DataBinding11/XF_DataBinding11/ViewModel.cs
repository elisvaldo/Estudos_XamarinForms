﻿using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace XF_DataBinding11
{
    public enum Operacao
    {
        Soma,
        Subtracao,
        Multiplicacao,
        Divisao
    }

    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Operacao? _operacao;
        private int _numero1;
        private int _numero2;

        private int? _visor;

        public int? Visor
        {
            get { return _visor; }
            set
            {
                _visor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Visor)));
            }
        }

        public ICommand RealizaOperacaoCommand { get; }
        public ICommand InsereNumeroCommand { get; }
        public ICommand LimpaTudoCommand { get; }
        public ICommand InsereOperacaoCommand { get; }

        public ViewModel()
        {
            _numero1 = 0;
            _numero2 = 0;

            InsereNumeroCommand = new Command<int>(InsereNumero);
            LimpaTudoCommand = new Command(LimpaTudo);
            InsereOperacaoCommand = new Command<Operacao>(InsereOperacao);
            RealizaOperacaoCommand = new Command(RealizaOperacao);
        }

        private void InsereNumero(int numeroInserido)
        {
            if (_operacao == null)
            {
                _numero1 = _numero1 * 10 + numeroInserido;
                Visor = _numero1;
            }
            else
            {
                _numero2 = _numero2 * 10 + numeroInserido;
                Visor = _numero2;
            }
        }

        private void LimpaTudo()
        {
            _numero1 = 0;
            _numero2 = 0;
            _operacao = null;
            Visor = null;
        }

        private void InsereOperacao(Operacao operacao)
        {
            _operacao = operacao;
        }

        private void RealizaOperacao()
        {
            switch (_operacao)
            {
                case Operacao.Soma:
                    _numero1 = _numero1 + _numero2;
                    break;
                case Operacao.Subtracao:
                    _numero1 = _numero1 - _numero2;
                    break;
                case Operacao.Multiplicacao:
                    _numero1 = _numero1 * _numero2;
                    break;
                case Operacao.Divisao:
                    try
                    {
                        _numero1 = _numero1 / _numero2;
                    }
                    catch
                    {
                        _numero1 = 0;
                    }
                    break;
                case null:
                    return;
            }

            Visor = _numero1;
            _numero2 = 0;
            _operacao = null;
        }
    }
}
