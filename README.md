# MintAnimation

#### 介绍
这是一款轻量级动画插件，在保证性能的情况下能够灵活简单的为你的UI/GameObject添加多样的动画。同时也可以通过该插件快速制作自定义动画。

#### 如何添加到项目
将以下字段添加到Packages/manifest.json中，若文件中已声明了scopedRegistries和dependencies标签，在标签中额外增加

```
{
  "scopedRegistries": [
    {
      "name": "Foldcc Tools",
      "url": "https://registry.npmjs.org",
      "scopes": [
        "com.foldcc"
      ]
    }
  ],
  "dependencies": {
    "com.foldcc.mintanimation": "0.1.1",
  }
}
```