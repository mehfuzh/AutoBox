/*
Language: TeX
Author: Vladimir Moskva <vladmos@gmail.com>
Website: http://fulc.ru/
*/

hljs.LANGUAGES.tex = {
  defaultMode: {
    contains: ['parameter', 'command', 'special', 'formula', 'comment']
  },
  modes: [
    {
      className: 'parameter',
      begin: '\\\\[a-zA-Zа-яА-я]+[\\*]? *= *-?\\d*\\.?\\d+(pt|pc|mm|cm|in|dd|cc|ex|em)?', end: hljs.IMMEDIATE_RE,
      returnBegin: true,
      contains: ['command', 'number'],
      noMarkup: true,
      relevance: 10
    },
    {
      className: 'command',
      begin: '\\\\[a-zA-Zа-яА-я]+[\\*]?', end: hljs.IMMEDIATE_RE,
      relevance: 10
    },
    {
      className: 'command',
      begin: '\\\\[^a-zA-Zа-яА-я0-9]', end: hljs.IMMEDIATE_RE,
      relevance: 0
    },
    {
      className: 'comment',
      begin: '%', end: '$',
      relevance: 0
    },
    {
      className: 'special',
      begin: '[{}\\[\\]\\&#~]', end: hljs.IMMEDIATE_RE,
      relevance: 0
    },
    {
      className: 'formula',
      begin: '\\$\\$', end: '\\$\\$',
      contains: ['command', 'special'],
      relevance: 0
    },
    {
      className: 'formula',
      begin: '\\$', end: '\\$',
      contains: ['command', 'special'],
      relevance: 0
    },
    {
      className: 'number',
      begin: ' *=', end: '-?\\d*\\.?\\d+(pt|pc|mm|cm|in|dd|cc|ex|em)?',
      excludeBegin: true
    }
  ]
};
