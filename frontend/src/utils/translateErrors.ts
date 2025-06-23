export function translateError(mensagem: string): string {
  if (mensagem.includes('Passwords must be at least 6 characters')) {
    return 'A senha deve ter pelo menos 6 caracteres.';
  }
  if (mensagem.includes('Passwords must have at least one digit')) {
    return 'A senha deve conter pelo menos um número.';
  }
  if (mensagem.includes('Passwords must have at least one lowercase')) {
    return 'A senha deve conter pelo menos uma letra minúscula.';
  }
  if (mensagem.includes('Passwords must have at least one uppercase')) {
    return 'A senha deve conter pelo menos uma letra maiúscula.';
  }
  if (mensagem.includes('Passwords must have at least one non alphanumeric character')) {
    return 'A senha deve conter pelo menos um caractere especial.';
  }

  return mensagem;
}

export function translateBackendErrors(erros: string | string[]): string {
  if (typeof erros === 'string') {
    return erros
      .split(';') 
      .map((msg) => translateError(msg.trim()))
      .join('\n');
  }

  if (Array.isArray(erros)) {
    return erros.map((msg) => translateError(msg)).join('\n');
  }

  return 'Erro desconhecido ao cadastrar.';
}
