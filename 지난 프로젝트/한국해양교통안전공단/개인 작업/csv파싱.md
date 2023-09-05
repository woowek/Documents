CSV 파싱
===

>개요
---
- CSV를 파싱한다.
- 구글링한 결과가 이상한 결과가 많아 여기 적는다.
- csv 파싱에는 쉼표, 쌍따옴표 처리 등 예외가 있다.
- 무조건적인 텍스트 변형엔 문제가 있어 오픈소스를 사용했다.


>d3.v3.js
---
- 주소 :  https://d3js.org/d3.v4.js
- 참조 필요시 : https://d3js.org/d3.v4.min.js
- 프로젝트에선 csv 파싱부분만 잘라서 사용
```js
    //csv 파싱 : d3.v3.js 에서 추출(No License)
    cmnComSmsAddress.dsv = function (delimiter, mimeType) {
        var reFormat = new RegExp('["' + delimiter + "\n]"), delimiterCode = delimiter.charCodeAt(0);
        function dsv(url, row, callback) {
            if (arguments.length < 3) callback = row, row = null;
            var xhr = d3_xhr(url, mimeType, row == null ? response : typedResponse(row), callback);
            xhr.row = function (_) {
                return arguments.length ? xhr.response((row = _) == null ? response : typedResponse(_)) : row;
            };
            return xhr;
        }
        function response(request) {
            return dsv.parse(request.responseText);
        }
        function typedResponse(f) {
            return function (request) {
                return dsv.parse(request.responseText, f);
            };
        }
        dsv.parse = function (text, f) {
            var o;
            return dsv.parseRows(text, function (row, i) {
                if (o) return o(row, i - 1);
                var a = new Function("d", "return {" + row.map(function (name, i) {
                    return JSON.stringify(name) + ": d[" + i + "]";
                }).join(",") + "}");
                o = f ? function (row, i) {
                    return f(a(row), i);
                } : a;
            });
        };
        dsv.parseRows = function (text, f) {
            var EOL = {}, EOF = {}, rows = [], N = text.length, I = 0, n = 0, t, eol;
            function token() {
                if (I >= N) return EOF;
                if (eol) return eol = false, EOL;
                var j = I;
                if (text.charCodeAt(j) === 34) {
                    var i = j;
                    while (i++ < N) {
                        if (text.charCodeAt(i) === 34) {
                            if (text.charCodeAt(i + 1) !== 34) break;
                            ++i;
                        }
                    }
                    I = i + 2;
                    var c = text.charCodeAt(i + 1);
                    if (c === 13) {
                        eol = true;
                        if (text.charCodeAt(i + 2) === 10) ++I;
                    } else if (c === 10) {
                        eol = true;
                    }
                    return text.slice(j + 1, i).replace(/""/g, '"');
                }
                while (I < N) {
                    var c = text.charCodeAt(I++), k = 1;
                    if (c === 10) eol = true; else if (c === 13) {
                        eol = true;
                        if (text.charCodeAt(I) === 10) ++I, ++k;
                    } else if (c !== delimiterCode) continue;
                    return text.slice(j, I - k);
                }
                return text.slice(j);
            }
            while ((t = token()) !== EOF) {
                var a = [];
                while (t !== EOL && t !== EOF) {
                    a.push(t);
                    t = token();
                }
                if (f && (a = f(a, n++)) == null) continue;
                rows.push(a);
            }
            return rows;
        };
        dsv.format = function (rows) {
            if (Array.isArray(rows[0])) return dsv.formatRows(rows);
            var fieldSet = new d3_Set(), fields = [];
            rows.forEach(function (row) {
                for (var field in row) {
                    if (!fieldSet.has(field)) {
                        fields.push(fieldSet.add(field));
                    }
                }
            });
            return [fields.map(formatValue).join(delimiter)].concat(rows.map(function (row) {
                return fields.map(function (field) {
                    return formatValue(row[field]);
                }).join(delimiter);
            })).join("\n");
        };
        dsv.formatRows = function (rows) {
            return rows.map(formatRow).join("\n");
        };
        function formatRow(row) {
            return row.map(formatValue).join(delimiter);
        }
        function formatValue(text) {
            return reFormat.test(text) ? '"' + text.replace(/\"/g, '""') + '"' : text;
        }
        return dsv;
    };
    cmnComSmsAddress.csv = cmnComSmsAddress.dsv(",", "text/csv");
```