Scaffold Style Guide
====================

Please observe the following patterns when submitting code changes to the Scaffold project.

**Comment Your Work**. Contrary to what may seem like popular belief, code is NOT self-explanatory. Please use comments so everyone can understand your perspective and intentions, especially in the documentation attribute sections. Remember: .NET applications are compiled. As a result, inline comments and other text won't ever affect the size of the executable.

**Full Braces**. Every statement with follow-up should be fully enclosed.
Instead of:
<blockquote><code><pre>
if(condition)
 doThis();
</pre></code></blockquote>
please use:
<blockquote><code><pre>
if(condition)
{
 doThis();
}
</pre></code></blockquote>

**Return At End**. Extra points are awarded for constructing your logic such that the return statement is always the last outer statement of the method and not using embedded or interrupting returns anywhere in the middle of the method.

**Variable Predefinition**. Extra points for inventorying and pre-initializing your variables at the beginning of each class or method.

**Tab Indentation**. Source files are tab-indented, not space-indented.

